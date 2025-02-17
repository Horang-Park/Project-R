using System;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Horang.HorangUnityLibrary.Foundation;
using Horang.HorangUnityLibrary.Utilities;
using UnityEngine;

namespace Managers
{
    public class FirebaseManager : MonoSingleton<FirebaseManager>
    {
        public struct FirebasePostActions
        {
            public readonly Action OnSuccess;
            public readonly Action OnCanceled;
            public readonly Action<string> OnFailed;

            public FirebasePostActions(Action onSuccess, Action onCanceled = null, Action<string> onFailed = null)
            {
                OnSuccess = onSuccess;
                OnCanceled = onCanceled;
                OnFailed = onFailed;
            }
        }

        private FirebaseApp _app;
        private FirebaseAuth _auth;

        public bool CheckDependencies()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;

                if (dependencyStatus == DependencyStatus.Available)
                {
                    _app = FirebaseApp.DefaultInstance;
                    _auth = FirebaseAuth.DefaultInstance;

                    return true;
                }

                Log.Print($"Could not resolve all Firebase dependencies: {dependencyStatus}");

                return false;
            });

            Log.Print("Check and fix dependencies all done.");

            return true;
        }

        public void AnonymouslyAuth(FirebasePostActions actions = default)
        {
            _auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Log.Print("SignInAnonymouslyAsync canceled.");

                    actions.OnCanceled?.Invoke();

                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);

                    actions.OnFailed?.Invoke(task.Exception?.Message);

                    return;
                }

                actions.OnSuccess?.Invoke();

                var result = task.Result;

                Log.Print($"Success: {result.User.DisplayName} / {result.User.UserId}");
            });
        }

        public void SetUserProfile(UserProfile profile, FirebasePostActions actions = default)
        {
            _auth.CurrentUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Log.Print("UpdateUserProfileAsync canceled.");

                    actions.OnCanceled?.Invoke();

                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);

                    actions.OnFailed?.Invoke(task.Exception?.Message);

                    return;
                }

                actions.OnSuccess?.Invoke();
            });
        }

        private void OnDestroy()
        {
            _auth.SignOut();
            _auth.Dispose(true);
        }
    }
}