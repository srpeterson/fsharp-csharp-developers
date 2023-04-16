
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

// We can compose measurment types
[<Measure>] type mph = mi / hr

let isSpeeding (speed: int<mph>)  =  speed > 75<mph>


