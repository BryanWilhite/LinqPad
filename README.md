# My LinqPad Queries Collection

This is my personal collection of [LinqPad](https://www.linqpad.net/) queries that I use to work out in C# (and, soon, F#). The public purpose of this repository is to set an example for beginner developers to make a _personal_ practice “normal” instead of depending on the current IT shop to “encourage” self-improvement/self-education.

## My LinqPad Query Highlights

* My [JSON.NET experiments](https://github.com/BryanWilhite/LinqPad/search?utf8=%E2%9C%93&q=Json.NET).
* My [NAudio experiments](https://github.com/BryanWilhite/LinqPad/tree/master/Queries/funkyKB/Audio).
* My [WPF research](https://github.com/BryanWilhite/LinqPad/tree/master/Queries/funkyKB/WPF).
* My [interview with Google](https://github.com/BryanWilhite/LinqPad/blob/master/Queries/funkyKB/Interview%20-%20Google.linq).
* My [interview with Microsoft](https://github.com/BryanWilhite/LinqPad/blob/master/Queries/funkyKB/Interview%20-%20Microsoft.linq).
* My interview with JPL ([part 1](https://github.com/BryanWilhite/LinqPad/blob/master/Queries/funkyKB/Interview%20-%20JPL%20(part%201).linq) and [part 2](https://github.com/BryanWilhite/LinqPad/blob/master/Queries/funkyKB/Interview%20-%20JPL%20(part%202).linq)).

## DLLs for Microsoft Expression Blend Technologies

Yes, [Microsoft Expression Blend](https://www.microsoft.com/expression) is somewhat buried in contemporary releases of Visual Studio. As of this writing, Blend in particular and WPF in general are not broken down into Core packages by Microsoft itself. It follows that developers copy DLLs from Microsoft (possibly violating licensing) and cram them into source control or we put or trust in 3<sup>rd</sup>-party NuGet packages such as these below:

* [`Microsoft.Expression.Blend.SDK.WPF`](https://www.nuget.org/packages/Microsoft.Expression.Blend.SDK.WPF/) with no description and its 2012 stamp, it is one of the oldest and most mysterious.
* [`Expression.Blend.Sdk`](https://www.nuget.org/packages/Expression.Blend.Sdk/) the description leads me assume that this one is the most promising.
* [`Expression.Blend.Sdk3`](https://www.nuget.org/packages/Expression.Blend.Sdk3/) this one appears older than the one above.

[@BryanWilhite](https://twitter.com/bryanwilhite)