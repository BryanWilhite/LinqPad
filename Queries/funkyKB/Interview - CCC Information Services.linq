<Query Kind="Statements" />

/*
    The following is an imperfect recollection
    of the only question in a multi-hour interview
    i failed to answer. This question was actually for SQL,
    higlighting the importance of `UNION`.

    Find all of the vehicles that have the following attributes:

    * has all-wheel drive
    * does not have lane-keeping assist
    * has a hatchback or a moon roof
*/

IEnumerable<KeyValuePair<int, string>> lkpDriveAssist = new Dictionary<int, string>
{
    { 1, "adaptive cruise control" },
    { 2, "lane-departure warning" },
    { 3, "lane-keeping assist" },
    { 4, "ludicrous assist" },
};

IEnumerable<KeyValuePair<int, string>> lkpDriveTrain = new Dictionary<int, string>
{
    { 1, "all-wheel drive" },
    { 2, "front-wheel drive" },
    { 3, "rear-wheel drive" },
};

IEnumerable<KeyValuePair<int, string>> lkpRearEntry = new Dictionary<int, string>
{
    { 1, "hatchback" },
    { 2, "lift gate" },
    { 3, "tailgate" },
    { 4, "trunk" },
};

IEnumerable<KeyValuePair<int, string>> lkpRoofEntry = new Dictionary<int, string>
{
    { 1, "none" },
    { 2, "moon roof" },
    { 3, "panorama roof" },
};

IEnumerable<KeyValuePair<int, string>> carCollection = new Dictionary<int, string>
{
    { 1, "elon-one" },
    { 2, "kanji-two" },
    { 3, "hangul-three" },
    { 4, "elon-four" },
};

IEnumerable<(int carId, int driveAssistId)> carDriveAssist = new[]
{
    (1, 4),
    (2, 3),
    (3, 1),
    (3, 2),
    (3, 3),
    (4, 4),
};

IEnumerable<(int carId, int driveTrainId)> carDriveTrain = new[]
{
    (1, 1),
    (2, 2),
    (3, 1),
    (3, 2),
    (3, 3),
    (4, 3),
};

IEnumerable<(int carId, int rearEntryId)> carRearEntry = new[]
{
    (1, 1),
    (1, 4),
    (2, 1),
    (2, 4),
    (3, 1),
    (4, 1),
};

IEnumerable<(int carId, int rearEntryId)> carRoofEntry = new[]
{
    (1, 3),
    (2, 1),
    (3, 1),
    (3, 2),
    (3, 3),
    (4, 3),
};
