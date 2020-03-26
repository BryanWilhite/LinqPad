<Query Kind="Program">
  <NuGetReference>NUnit</NuGetReference>
  <Namespace>NUnit.Framework</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
    var tests = new UnitTests();
    tests.CalculateCarValue();
}

public static class UsedCarExtensions
{
    public static UsedCar ToUsedCar(this Car car)
    {
        var jO = JObject.FromObject(car);
        return JsonConvert.DeserializeObject<UsedCar>(jO.ToString());
    }

    public static UsedCar WithAgeReduction(this UsedCar car)
    {
        _ = car ?? throw new ArgumentNullException(nameof(car));

        const decimal reductionPercentage = 0.005m;
        const int tenYearsInMonths = 120;

        var reductionFactor = (car.AgeInMonths > tenYearsInMonths) ?
            tenYearsInMonths
            :
            car.AgeInMonths;

        var reducedPrice = car.UsedValue * reductionPercentage;

        car.UsedValue -= reducedPrice * reductionFactor;

        return car;
    }

    public static UsedCar WithCollisionReduction(this UsedCar car)
    {
        _ = car ?? throw new ArgumentNullException(nameof(car));

        const decimal reductionPercentage = 0.02m;
        const int maxCollisions = 5;

        var reductionFactor = (car.NumberOfCollisions > maxCollisions) ?
            maxCollisions
            :
            car.NumberOfCollisions;

        var reducedPrice = car.UsedValue * reductionPercentage;

        car.UsedValue -= reducedPrice * reductionFactor;

        return car;
    }

    public static UsedCar WithMilesReduction(this UsedCar car)
    {
        _ = car ?? throw new ArgumentNullException(nameof(car));

        const decimal reductionPercentage = 0.002m;
        const int maxMiles = 150000;

        var reductionFactor = ((car.NumberOfMiles > maxMiles) ?
            maxMiles
            :
            car.NumberOfMiles) / 1000M;

        var reducedPrice = car.UsedValue * reductionPercentage;

        car.UsedValue -= reducedPrice * reductionFactor;

        return car;
    }

    public static UsedCar WithPreviousOwnerBonus(this UsedCar car)
    {
        _ = car ?? throw new ArgumentNullException(nameof(car));

        const decimal bonusPercentage = 0.10m;

        if (car.NumberOfPreviousOwners > 0) return car;

        var bonusPrice = car.UsedValue * bonusPercentage;

        car.UsedValue += bonusPrice;

        return car;
    }

    public static UsedCar WithPreviousOwnerReduction(this UsedCar car)
    {
        _ = car ?? throw new ArgumentNullException(nameof(car));

        const decimal reductionPercentage = 0.25m;
        const int maxOwners = 2;

        if(car.NumberOfPreviousOwners <= maxOwners) return car;

        var reducedPrice = car.UsedValue * reductionPercentage;

        car.UsedValue -= reducedPrice;

        return car;
    }

    public static UsedCar WithOriginalPurchasePrice(this UsedCar car)
    {
        _ = car ?? throw new ArgumentNullException(nameof(car));

        car.UsedValue = car.PurchaseValue;

        return car;
    }
}

#region Instructions
/*
 * You are tasked with writing an algorithm that determines the value of a used car, 
 * given several factors.
 * 
 *    AGE:    Given the number of months of how old the car is, reduce its value one-half 
 *            (0.5) percent.
 *            After 10 years, it's value cannot be reduced further by age. This is not 
 *            cumulative.
 *            
 *    MILES:    For every 1,000 miles on the car, reduce its value by one-fifth of a
 *              percent (0.2). Do not consider remaining miles. After 150,000 miles, it's 
 *              value cannot be reduced further by miles.
 *            
 *    PREVIOUS OWNER:    If the car has had more than 2 previous owners, reduce its value 
 *                       by twenty-five (25) percent. If the car has had no previous  
 *                       owners, add ten (10) percent of the FINAL car value at the end.
 *                    
 *    COLLISION:        For every reported collision the car has been in, remove two (2) 
 *                      percent of it's value up to five (5) collisions.
 *                    
 * 
 *    Each factor should be off of the result of the previous value in the order of
 *        1. AGE
 *        2. MILES
 *        3. PREVIOUS OWNER
 *        4. COLLISION
 *        
 *    E.g., Start with the current value of the car, then adjust for age, take that  
 *    result then adjust for miles, then collision, and finally previous owner. 
 *    Note that if previous owner, had a positive effect, then it should be applied 
 *    AFTER step 4. If a negative effect, then BEFORE step 4.
 */
#endregion

public class UsedCar : Car
{
    public decimal UsedValue { get; set; }
}

public class Car
{
    public decimal PurchaseValue { get; set; }
    public int AgeInMonths { get; set; }
    public int NumberOfMiles { get; set; }
    public int NumberOfPreviousOwners { get; set; }
    public int NumberOfCollisions { get; set; }
}

public class PriceDeterminator
{
    public decimal DetermineCarPrice(Car car)
    {
        var usedCar = car
            .ToUsedCar()
            .WithOriginalPurchasePrice()
            .WithAgeReduction()
            .WithMilesReduction()
            .WithPreviousOwnerReduction()
            .WithCollisionReduction()
            .WithPreviousOwnerBonus();

        return usedCar.UsedValue;
    }
}


public class UnitTests
{

    public void CalculateCarValue()
    {
        AssertCarValue(25313.40m, 35000m, 3 * 12, 50000, 1, 1);
        AssertCarValue(19688.20m, 35000m, 3 * 12, 150000, 1, 1);
        AssertCarValue(19688.20m, 35000m, 3 * 12, 250000, 1, 1);
        AssertCarValue(20090.00m, 35000m, 3 * 12, 250000, 1, 0);
        AssertCarValue(21657.02m, 35000m, 3 * 12, 250000, 0, 1);
    }

    private static void AssertCarValue(decimal expectedValue, decimal purchaseValue,
    int ageInMonths, int numberOfMiles, int numberOfPreviousOwners, int
    numberOfCollisions)
    {
        Car car = new Car
        {
            AgeInMonths = ageInMonths,
            NumberOfCollisions = numberOfCollisions,
            NumberOfMiles = numberOfMiles,
            NumberOfPreviousOwners = numberOfPreviousOwners,
            PurchaseValue = purchaseValue
        };
        PriceDeterminator priceDeterminator = new PriceDeterminator();
        var carPrice = priceDeterminator.DetermineCarPrice(car);

        $"expected: {expectedValue}, actual: {carPrice}".Dump();

        Assert.AreEqual(expectedValue, carPrice);
    }
}

// 📚 https://thycotic.com/about-us/careers/code-example/