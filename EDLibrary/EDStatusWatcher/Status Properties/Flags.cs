namespace EDLibrary.EDStatusWatcher
{
    public enum Flags
    {
        DOCKED_LANDINGPAD = (1 << 0),
        LANDED_SURFACE = (1 << 1),
        LANDING_GEAR_DOWN = (1 << 2),
        SHIELDS_UP = (1 << 3),
        SUPERCRUISE = (1 << 4),
        FLIGHTASSIST_OFF = (1 << 5),
        HARDPOINTS_DEPLOYED = (1 << 6),
        IN_WING = (1 << 7),
        LIGHT_ON = (1 << 8),
        CARGO_SCOOP_DEPLOYED = (1 << 9),
        SILENT_RUNNING = (1 << 10),
        SCOOPING_FUEL = (1 << 11),
        SRV_HANDBREAK = (1 << 12),
        SRV_USING_TURRET = (1 << 13),
        SRV_TURRET_RETRACTED = (1 << 14),
        SRV_DRIVEASSIS = (1 << 15),
        FSD_MASSLOCKED = (1 << 16),
        PSD_CHARGING = (1 << 17),
        FSD_COOLDOWN = (1 << 18),
        LOW_FUEL_25 = (1 << 19),
        OVER_HEARTING_100 = (1 << 20),
        HAS_LAT_LONG = (1 << 21),
        IS_IN_DANGER = (1 << 22),
        BEING_INTERDICTED = (1 << 23),
        IN_MAINSHIP = (1 << 24),
        IN_FIGHTER = (1 << 25),
        IN_SRV = (1 << 26),
        HUD_IN_ANALYSIS_MODE = (1 << 27),
        NIGHT_VISION = (1 << 28),
        ALTITUDE_FROM_AVARAGE_RADIUS = (1 << 29),
        FSD_JUMP = (1 << 30),
        SRV_HIGH_BEAM = (1 << 31)
    };
}
