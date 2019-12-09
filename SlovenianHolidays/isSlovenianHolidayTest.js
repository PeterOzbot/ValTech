const isSloveniaHoliday = require('./isSloveniaHoliday');

// should be "VELIKONOČNI PONEDELJEK"
let date = new Date(2020, 3, 13);

let holiday = isSloveniaHoliday(date);
if (holiday) {
    console.log(holiday.name);
}
else {
    console.log("Not a holiday.");
}