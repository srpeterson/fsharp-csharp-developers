
// The built in [<Measure>] attribute allows us
// to assign meta data to any floating point or integer value.
// This is very helpfull when you need to distinguish between
// the same data types when doing things such as conversions

// Hour
[<Measure>] type hr

// Kilometer
[<Measure>] type km

// Mile
[<Measure>] type mi

//convert miles to kilometers
let milesToKilometers (miles: float<mi>) = miles * 1.61<km>

//convert kilometers to miles
let kilometersToMiles (kilometers: float<km>) = kilometers * 0.621<mi>

let isSpeeding (miles: int<mi>)  =  (miles / 1<hr>) > (75<mi> / 1<hr>)

// This is a bit clunky, but fortunately in F# 
// we can compose measurment types
[<Measure>] type mph = mi / hr

let newIsSpeeding (speed: int<mph>)  =  speed > 75<mph>
