using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayGames : MonoBehaviour
{
    public Text playerScore;
    public GameObject winPopup;

    string leaderboardID = "CgkImpXwuagaEAIQAQ";
    string easy_achievementID = "CgkImpXwuagaEAIQAg";
    //string medium_achievementID = "CgkI3KKQmOkcEAIQAg";
    //string hard_achievementID = "CgkI3KKQmOkcEAIQAw";
    //string veryhard_achievementID = "CgkI3KKQmOkcEAIQBA";
    //string all_achievementID = "CgkI3KKQmOkcEAIQBQ";
    //string veryfast_achievementID = "CgkI3KKQmOkcEAIQBg";
    //string fast_achievementID = "CgkI3KKQmOkcEAIQBw";

    public static PlayGamesPlatform platform;
    //private string gameMode = GameSettings.Instance.GetGameMode();

    private int all_dif_total = 0;

    void Start()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Logged in successfully");
            }
            else
            {
                Debug.Log("Login Failed");
            }
        });

        //Social.LoadAchievements(achievements => {
        //    if (achievements.Length > 0)
        //    {
        //        Debug.Log("Got " + achievements.Length + " achievement instances");
        //        string myAchievements = "My achievements:\n";
                
        //        foreach (IAchievement achievement in achievements)
        //        {
        //            myAchievements += "\t" +
        //                achievement.id + " " +
        //                achievement.percentCompleted + " " +
        //                achievement.completed + " " +
        //                achievement.lastReportedDate + "\n";

        //            if (achievement.id == "CgkI3KKQmOkcEAIQAQ" && achievement.completed == true)
        //                all_dif_total += 1;
        //            if (achievement.id == "CgkI3KKQmOkcEAIQAg" && achievement.completed == true)
        //                all_dif_total += 2;
        //            if (achievement.id == "CgkI3KKQmOkcEAIQAw" && achievement.completed == true)
        //                all_dif_total += 3;
        //            if (achievement.id == "CgkI3KKQmOkcEAIQBA" && achievement.completed == true)
        //                all_dif_total += 4;
        //        }
        //        Debug.Log(myAchievements);
        //    }
        //    else
        //        Debug.Log("No achievements returned");
        //});
    }

    public void AddScoreToLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportScore(int.Parse(playerScore.text), leaderboardID, success => { });
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowLeaderboardUI();
        }
    }

    public void ShowAchievements()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowAchievementsUI();
        }
    }

    public void UnlockAchievement()
    {
        if (Social.Active.localUser.authenticated)
        {
            if(winPopup.active == true)
            {
                //if (gameMode == "Easy")
                    Social.ReportProgress(easy_achievementID, 100f, success => { });
                //else if (gameMode == "Medium")
                //    Social.ReportProgress(medium_achievementID, 100f, success => { });
                //else if (gameMode == "Hard")
                //    Social.ReportProgress(hard_achievementID, 100f, success => { });
                //else if (gameMode == "VeryHard")
                //    Social.ReportProgress(veryhard_achievementID, 100f, success => { });

                //if(Clock.instance.delta_time <= 60)
                //    Social.ReportProgress(veryfast_achievementID, 100f, success => { });
                //else if (Clock.instance.delta_time <= 100)
                //    Social.ReportProgress(fast_achievementID, 100f, success => { });

                //if (all_dif_total >= 10)
                //    Social.ReportProgress(all_achievementID, 100f, success => { });
            }
        }
    }
}

