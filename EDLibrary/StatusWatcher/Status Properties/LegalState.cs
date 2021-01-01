namespace EDLibrary.StatusWatcher
{
    /// <summary>
    /// All possible legal states of Elite Dangerous
    /// </summary>
    public enum LegalState
    {
        NULL,
        CLEAN,
        ILLEGAL_CARGO,
        SPEEDING,
        WANTED,
        HOSTILE,
        PASSANGER_WANTED,
        WARRANT
    }
}
