using FMOD.Studio;
using FMODUnity;

public static class StaticFmodRefs
{
    public static class AppRefs
    {
        public static string PATH_MUSIC;

        public static string PATH_MENU_NAVIGATE;
        public static string PATH_MENU_SELECT;
        public static string PATH_MENU_RETURN;
    }



    public static class SoccerRefs
    {
        public static string PATH_MUSIC = "event:/GAME Soccer/Music";

        public static string PATH_LEVEL_START = "event:/GAME Soccer/Apito";

        public static string PATH_ANSWER = "event:/GAME Soccer/Answer";

        public static string PATH_FEEDBACK = "event:/GAME Soccer/Feedback";
        public static string PARAMETER_FEEDBACK = "Feedback";


        public static string PATH_KICK_DEFAULT = "event:/GAME Soccer/Chute";
        public static string PATH_KICK_QUICK = "event:/GAME Soccer/Chute de fogo";


        public static string PATH_TIMER_RUNNINGOUT = "event:/GAME Soccer/Tempo acabando";
        public static string PATH_TIMER_OVER = "event:/GAME Soccer/Tempo acabou";


        public static string PATH_BOSS_LAUGH = "event:/GAME Soccer/Boss/Boss risada";
        public static string PATH_BOSS_CRY = "event:/GAME Soccer/Boss/Boss triste";
        public static string PATH_BOSS_PREPARE = "event:/GAME Soccer/Boss/Boss preparação";


        public static string PATH_ROCKET_LAUNCH = "";
        public static string PATH_ROCKET_STOP = "";
        public static string PATH_ROCKET_RISE = "";

    }



    public static class CauldronRefs
    {

    }



    #region Methods



    public static PARAMETER_ID GetID(EventInstance eventInstance, string paramName)
    {
        eventInstance.getDescription(out EventDescription eventDescription);
        eventDescription.getParameterDescriptionByName(paramName, out PARAMETER_DESCRIPTION paramDescription);
        return paramDescription.id;
    }

    public static PARAMETER_ID GetGlobalID(string paramName)
    {
        RuntimeManager.StudioSystem.getParameterDescriptionByName(paramName, out PARAMETER_DESCRIPTION paramDescription);
        return paramDescription.id;
    }



    #endregion
}