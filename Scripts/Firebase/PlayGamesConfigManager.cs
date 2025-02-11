using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using Firebase.Auth;
using Firebase;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayGamesConfigManager : MonoBehaviour, ISocialPlatform
{
    private string authCode;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] GameObject LoadingPanel;
    public ILocalUser localUser => throw new NotImplementedException();

    public static PlayGamesConfigManager instance { get; private set; }
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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UserGooglePlayLogin(FirebaseAuth auth)
    {
        try
        {
            PlayGamesPlatform.Instance.Authenticate(status =>
            {
                if (status == SignInStatus.Success)
                {
                    try
                    {
                        PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                        {                        

                            Credential credential = PlayGamesAuthProvider.GetCredential(code);

                            StartCoroutine(AuthGet());

                            IEnumerator AuthGet()
                            {
                                Task<FirebaseUser> task = auth.SignInWithCredentialAsync(credential);

                                while (task.IsCompleted)
                                {
                                    yield return null;
                                }

                                if (task.IsCanceled)
                                {
                                    //Cancel
                                    textMeshPro.text += "Auth Cancelled";
                                }
                                else if (task.IsFaulted)
                                {
                                    //task.exeption
                                    textMeshPro.text += "Fauled => " + task.Exception.ToString();
                                }
                                else
                                {
                                    FirebaseUser newUser = task.Result;
                                    auth.SignInWithEmailAndPasswordAsync(newUser.Email, newUser.UserId);
                                    textMeshPro.text += newUser.ToString();
                                    CreateNewLoading();
                                }
                            }
                        });
                    }
                    catch (System.Exception e)
                    {

                        textMeshPro.text += "Exception => " + e.Message;
                    }


                }
                else
                {
                    textMeshPro.text = "Auth Failed!";

                    // <Testlerden sonra silineccek kodlar>
                    CreateNewLoading();
                    // </Testlerden sonra silineccek kodlar>
                }
            });
        }
        catch (System.Exception e)
        {
            textMeshPro.text += e.Message;
            throw;
        }
    }
    public void CreateNewLoading()
    {
        Transform canvas = FindObjectOfType<Canvas>().gameObject.transform;
        Instantiate(LoadingPanel, canvas);
    }
    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            PlayGamesPlatform.Instance.RequestServerSideAccess(
    /* forceRefreshToken= */ false,
            code=> {
                // send code to server
                PlayGamesPlatform.Instance.localUser.Authenticate((bool success) => {
                    if (success)
                    {
                        authCode = code;
                    }
                });
            });
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
    public void PLayGamesConfig()
    {
        PlayGamesPlatform.Activate();
    }
    public void TryToLogIn()
    {
        

        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.Credential credential =
            Firebase.Auth.PlayGamesAuthProvider.GetCredential(authCode);
        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    public void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
    {
        throw new NotImplementedException();
    }

    public void ReportProgress(string achievementID, double progress, Action<bool> callback)
    {
        throw new NotImplementedException();
    }

    public void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
    {
        throw new NotImplementedException();
    }

    public void LoadAchievements(Action<IAchievement[]> callback)
    {
        throw new NotImplementedException();
    }

    public IAchievement CreateAchievement()
    {
        throw new NotImplementedException();
    }

    public void ReportScore(long score, string board, Action<bool> callback)
    {
        throw new NotImplementedException();
    }

    public void LoadScores(string leaderboardID, Action<IScore[]> callback)
    {
        throw new NotImplementedException();
    }

    public ILeaderboard CreateLeaderboard()
    {
        throw new NotImplementedException();
    }

    public void ShowAchievementsUI()
    {
        throw new NotImplementedException();
    }

    public void ShowLeaderboardUI()
    {
        throw new NotImplementedException();
    }

    public void Authenticate(ILocalUser user, Action<bool> callback)
    {
        throw new NotImplementedException();
    }

    public void Authenticate(ILocalUser user, Action<bool, string> callback)
    {
        throw new NotImplementedException();
    }

    public void LoadFriends(ILocalUser user, Action<bool> callback)
    {
        throw new NotImplementedException();
    }

    public void LoadScores(ILeaderboard board, Action<bool> callback)
    {
        throw new NotImplementedException();
    }

    public bool GetLoading(ILeaderboard board)
    {
        throw new NotImplementedException();
    }
}
