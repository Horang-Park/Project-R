using System;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Horang.HorangUnityLibrary.Foundation;
using Horang.HorangUnityLibrary.Utilities;
using Newtonsoft.Json;
using UnityEngine;

namespace Managers
{
    public class FirebaseManager : MonoSingleton<FirebaseManager>
    {
        #region Callback classes
        public abstract class BaseFirebaseCallback
        {
            public Action OnCanceled;
            public Action<string> OnFailed;
        }

        public class CommonFirebaseCallback : BaseFirebaseCallback
        {
            public readonly Action OnSuccess;

            public CommonFirebaseCallback(Action onSuccess, Action onCanceled = null, Action<string> onFailed = null)
            {
                OnSuccess = onSuccess;
                OnCanceled = onCanceled;
                OnFailed = onFailed;
            }
        }

        public class GetValueFirebaseCallback : BaseFirebaseCallback
        {
            public readonly Action<object> OnSuccess;

            public GetValueFirebaseCallback(Action<object> onSuccess, Action onCanceled = null, Action<string> onFailed = null)
            {
                OnSuccess = onSuccess;
                OnCanceled = onCanceled;
                OnFailed = onFailed;
            }
        }
        #endregion

        #region Serialize structures
        private struct User
        {
            public string DisplayName;
            public string ProfileImageUrl;

            public User(string displayName, string profileImageUrl)
            {
                DisplayName = displayName;
                ProfileImageUrl = profileImageUrl;
            }
        }
        #endregion

        public bool IsUserDisplayNameNullOrEmpty => string.IsNullOrEmpty(_auth.CurrentUser.DisplayName) || string.IsNullOrWhiteSpace(_auth.CurrentUser.DisplayName);

        private FirebaseApp _app;
        private FirebaseAuth _auth;
        private DatabaseReference _databaseReference;

        public void CheckDependencies()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;

                if (dependencyStatus == DependencyStatus.Available)
                {
                    _app = FirebaseApp.DefaultInstance;
                    _auth = FirebaseAuth.DefaultInstance;
                    _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                    Log.Print("Check and fix dependencies all done.");
                }
                else
                {
                    Log.Print($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }

        #region Authentication functions
        public void AnonymouslyAuth(CommonFirebaseCallback actions = null)
        {
            _auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Log.Print("SignInAnonymouslyAsync canceled.");

                    actions?.OnCanceled?.Invoke();

                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);

                    actions?.OnFailed?.Invoke(task.Exception?.Message);

                    return;
                }

                actions?.OnSuccess?.Invoke();

                var result = task.Result;

                Log.Print($"Success: {result.User.DisplayName} / {result.User.UserId}");
            });
        }

        public void SetUserProfile(UserProfile profile, CommonFirebaseCallback actions = null)
        {
            _auth.CurrentUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Log.Print("UpdateUserProfileAsync canceled.");

                    actions?.OnCanceled?.Invoke();

                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);

                    actions?.OnFailed?.Invoke(task.Exception?.Message);

                    return;
                }

                actions?.OnSuccess?.Invoke();
            });
        }
        #endregion

        #region Realtime database functions
        public void AddUser(string displayName, string profileImageUrl, CommonFirebaseCallback actions = null)
        {
            var data = new User(displayName, profileImageUrl);
            var json = JsonConvert.SerializeObject(data);

            _databaseReference.Child("users")
                .Child(_auth.CurrentUser.UserId)
                .SetRawJsonValueAsync(json)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled)
                    {
                        Log.Print("Add user canceled.");

                        actions?.OnCanceled?.Invoke();

                        return;
                    }

                    if (task.IsFaulted)
                    {
                        Debug.LogError("Add user encountered an error: " + task.Exception);

                        actions?.OnFailed?.Invoke(task.Exception?.Message);

                        return;
                    }

                    actions?.OnSuccess?.Invoke();
                });
        }

        public void SetHighScore(int score, CommonFirebaseCallback actions = null)
        {
            _databaseReference.Child("users")
                .Child(_auth.CurrentUser.UserId)
                .Child("HighScore")
                .SetValueAsync(score.ToString())
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled)
                    {
                        Log.Print("Add user canceled.");

                        actions?.OnCanceled?.Invoke();

                        return;
                    }

                    if (task.IsFaulted)
                    {
                        Debug.LogError("Add user encountered an error: " + task.Exception);

                        actions?.OnFailed?.Invoke(task.Exception?.Message);

                        return;
                    }

                    actions?.OnSuccess?.Invoke();
                });
        }

        public void GetHighScore(GetValueFirebaseCallback actions = null)
        {
            _databaseReference.Child("users")
                .Child(_auth.CurrentUser.UserId)
                .Child("HighScore")
                .GetValueAsync()
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled)
                    {
                        Log.Print("Add user canceled.");

                        actions?.OnCanceled?.Invoke();

                        return;
                    }

                    if (task.IsFaulted)
                    {
                        Debug.LogError("Add user encountered an error: " + task.Exception);

                        actions?.OnFailed?.Invoke(task.Exception?.Message);

                        return;
                    }

                    if (task.IsCompleted)
                    {
                        actions?.OnSuccess?.Invoke(task.Result.Value);
                    }
                });
        }
        #endregion

        #region Test functions
        public void SignOut()
        {
            _auth.SignOut();
        }
        #endregion

        private void OnDestroy()
        {
            _auth.Dispose(true);
        }
    }
}