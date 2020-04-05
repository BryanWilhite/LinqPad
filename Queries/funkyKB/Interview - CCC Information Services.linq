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

IEnumerable<(int driveAssistId, string name)> lkpDriveAssist = new[]
{
    ( 1, "adaptive cruise control" ),
    ( 2, "lane-departure warning" ),
    ( 3, "lane-keeping assist" ),
    ( 4, "ludicrous assist" ),
};

IEnumerable<(int driveTrainId, string name)> lkpDriveTrain = new[]
{
    ( 1, "all-wheel drive" ),
    ( 2, "front-wheel drive" ),
    ( 3, "rear-wheel drive" ),
};

IEnumerable<(int rearEntryId, string name)> lkpRearEntry = new[]
{
    ( 1, "hatchback" ),
    ( 2, "lift gate" ),
    ( 3, "tailgate" ),
    ( 4, "trunk" ),
};

IEnumerable<(int roofEntryId, string name)> lkpRoofEntry = new[]
{
    ( 1, "none" ),
    ( 2, "moon roof" ),
    ( 3, "panorama roof" ),
};

IEnumerable<(int carId, string name)> carCollection = new[]
{
    ( 1, "elon-one" ),
    ( 2, "kanji-two" ),
    ( 3, "hangul-three" ),
    ( 4, "elon-four" ),
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

IEnumerable<(int carId, int roofEntryId)> carRoofEntry = new[]
{
    (1, 3),
    (2, 1),
    (3, 1),
    (3, 2),
    (3, 3),
    (4, 3),
};

var joinDriveAssist = carCollection
    .Join(
        carDriveAssist,
        car => car.carId,
        m2m => m2m.carId,
        (car, m2m) => new { car.carId, carName = car.name, m2m.driveAssistId })
    .Where(join => join.driveAssistId != 3)
    .Join(lkpDriveAssist,
        join => join.driveAssistId,
        lkp => lkp.driveAssistId,
        (join, lkp) => new { join.carId, join.carName, lkp.name });

// 📖 https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.join?view=netstandard-2.1

var joinDriveTrain = carCollection
    .Join(
        carDriveTrain,
        car => car.carId,
        m2m => m2m.carId,
        (car, m2m) => new { car.carId, carName = car.name, m2m.driveTrainId })
    .Where(join => join.driveTrainId == 1)
    .Join(lkpDriveTrain,
        join => join.driveTrainId,
        lkp => lkp.driveTrainId,
        (join, lkp) => new { join.carId, join.carName, lkp.name });

var joinRearEntry = carCollection
    .Join(
        carRearEntry,
        car => car.carId,
        m2m => m2m.carId,
        (car, m2m) => new { car.carId, carName = car.name, m2m.rearEntryId })
    .Where(join => join.rearEntryId == 1)
    .Join(lkpRearEntry,
        join => join.rearEntryId,
        lkp => lkp.rearEntryId,
        (join, lkp) => new { join.carId, join.carName, lkp.name });

var joinRoofEntry = carCollection
    .Join(
        carRoofEntry,
        car => car.carId,
        m2m => m2m.carId,
        (car, m2m) => new { car.carId, carName = car.name, m2m.roofEntryId })
    .Where(join => join.roofEntryId == 2)
    .Join(lkpRoofEntry,
        join => join.roofEntryId,
        lkp => lkp.roofEntryId,
        (join, lkp) => new { join.carId, join.carName, lkp.name });

joinDriveAssist
    .Union(joinDriveTrain)
    .Union(joinRearEntry)
    .Union(joinRoofEntry)
    .OrderBy(union => union.carName)
    .ThenBy(union => union.name)
    .Dump();