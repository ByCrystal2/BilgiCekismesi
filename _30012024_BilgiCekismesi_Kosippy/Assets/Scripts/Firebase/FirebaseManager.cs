using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);        
    }
    private void Start()
    {
        InitializeFirebase();
    }
    FirebaseAuth auth;
    FirebaseUser user;

    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        PlayGamesConfigManager.instance.PLayGamesConfig();
        PlayGamesConfigManager.instance.UserGooglePlayLogin(auth);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }
    public void GetProfile()
    {
        if (user != null)
        {
            string name = user.DisplayName;
            string email = user.Email;
            System.Uri photo_url = user.PhotoUrl;
            // The user's Id, unique to the Firebase project.
            // Do NOT use this value to authenticate with your backend server, if you
            // have one; use User.TokenAsync() instead.
            string uid = user.UserId;
        }
    }
    public void GetSpecialProfile()
    {        
        if (user != null)
        {
            foreach (var profile in user.ProviderData)
            {
                // Id of the provider (ex: google.com)
                string providerId = profile.ProviderId;

                // UID specific to the provider
                string uid = profile.UserId;

                // Name, email address, and profile photo Url
                string name = profile.DisplayName;
                string email = profile.Email;
                System.Uri photoUrl = profile.PhotoUrl;
            }
        }
    }
    public void ProfileUpdating()
    {
        if (user != null)
        {
            UserProfile profile = new UserProfile
            {
                DisplayName = "Jane Q. User",
                PhotoUrl = new System.Uri("https://example.com/jane-q-user/profile.jpg"),
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");
            });
        }
    }
    public void UpdateEmail()
    {
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            user.UpdateEmailAsync("user@example.com").ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateEmailAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateEmailAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User email updated successfully.");
            });
        }
    }
    public void SendEmailVerification()
    {
        if (user != null)
        {
            user.SendEmailVerificationAsync().ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SendEmailVerificationAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SendEmailVerificationAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("Email sent successfully.");
            });
        }
    }
    public void UpdatePassword()
    {
        string newPassword = "SOME-SECURE-PASSWORD";
        if (user != null)
        {
            user.UpdatePasswordAsync(newPassword).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdatePasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdatePasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("Password updated successfully.");
            });
        }
    }
    public void SendPasswordResetEmail()
    {
        string emailAddress = "user@example.com";
        if (user != null)
        {
            auth.SendPasswordResetEmailAsync(emailAddress).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("Password reset email sent successfully.");
            });
        }
    }
    public void DeleteUSer()
    {
        if (user != null)
        {
            user.DeleteAsync().ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("DeleteAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("DeleteAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User deleted successfully.");
            });
        }
    }
    public void Reauthenticate()
    {

        // Get auth credentials from the user for re-authentication. The example below shows
        // email and password credentials but there are multiple possible providers,
        // such as GoogleAuthProvider or FacebookAuthProvider.
            Credential credential = EmailAuthProvider.GetCredential("user@example.com", "password1234");

        if (user != null)
        {
            user.ReauthenticateAsync(credential).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("ReauthenticateAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("ReauthenticateAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User reauthenticated successfully.");
            });
        }
    }
    public string GetPlayerFirebaseUID()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null && user.IsValid())
        {
            string playerName = user.DisplayName;

            // The user's Id, unique to the Firebase project.
            // Do NOT use this value to authenticate with your backend server, if you
            // have one; use User.TokenAsync() instead.
            string uid = user.UserId;
            return uid;
        }
        else
        {
            Debug.Log("User is not valid");
            return "User is not valid";
        }
    }
    // Handle removing subscription and reference to the Auth instance.
    // Automatically called by a Monobehaviour after Destroy is called on it.
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
}
public class User
{
    public string email;
    public string password;
    public string displayName;
    public System.Uri photoUrl;
}
