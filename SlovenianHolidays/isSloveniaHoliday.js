// function checks if date is a slovenia day off holiday
// most holidays are hard-coded, easter is calculated
// based on idea found here: http://www.whydomath.org/Reading_Room_Material/ian_stewart/2000_03.html

const staticHolidays = [
    { month: 0, day: 1, name: "NOVO LETO" },
    { month: 1, day: 8, name: "SLOVENSKI KULTURNI PRAZNIK" },
    { month: 3, day: 27, name: "DAN UPORA PROTI OKUPATORJU" },
    { month: 4, day: 1, name: "PRAZNIK DELA" },
    { month: 4, day: 2, name: "PRAZNIK DELA" },
    { month: 5, day: 25, name: "DAN DRŽAVNOSTI" },
    { month: 7, day: 15, name: "MARIJINO VNEBOVZETJE" },
    { month: 9, day: 31, name: "DAN REFORMACIJE" },
    { month: 10, day: 1, name: "DAN SPOMINA NA MRTVE" },
    { month: 11, day: 25, name: "BOŽIČ" },
    { month: 11, day: 26, name: "DAN SAMOSTOJNOSTI IN ENOTNOSTI" }
];

// checks if date is holiday in SLOVENIA
function isSloveniaHoliday(date) {

    // get day, month and year
    let day = date.getDate();
    let month = date.getMonth();
    let year = date.getFullYear();

    // check static holidays
    let holiday = staticHolidays.find(holiday => holiday.month == month && holiday.day == day);
    if (holiday) {
        return holiday;
    }

    // calculate the date of easter from date's year
    let easter = getEasterMondayDate(year);
    if (easter && easter.day == day && easter.month == month) {
        return easter;
    }

    // its not holiday
    return null;
};

// calculate easter monday date
// http://www.whydomath.org/Reading_Room_Material/ian_stewart/2000_03.html
function getEasterMondayDate(year) {

    // Divide x by 19 to get a quotient (which we ignore) and a remainder A. This is the year’s position in the 19-year lunar cycle. (A + 1 is the year’s Golden Number.)
    var a = year % 19;

    // Divide x by 100 to get a quotient B and a remainder C.    
    var b = Math.floor(year / 100);
    var c = year % 100;

    // Divide B by 4 to get a quotient D and a remainder E.
    var d = Math.floor(b / 4);
    var e = b % 4;

    // Divide 8B + 13 by 25 to get a quotient G and a remainder (which we ignore).
    var g = Math.floor((8 * b + 13) / 25);

    // Divide 19A + B – D – G + 15 by 30 to get a quotient (which we ignore) and a remainder H. (The year’s Epact is 23 – H when H is less than 24 and 53 – H otherwise.)
    var h = (19 * a + b - d - g + 15) % 30;

    // Divide A + 11H by 319 to get a quotient M and a remainder (which we ignore).
    var m = Math.floor((a + 11 * h) / 319);

    // Divide C by 4 to get a quotient J and a remainder K.
    var j = Math.floor(c / 4);
    var k = c % 4;

    // Divide 2E + 2J – K – H + M + 32 by 7 to get a quotient (which we ignore) and a remainder L.
    var l = (2 * e + 2 * j - k - h + m + 32) % 7;

    //  Divide H – M + L + 90 by 25 to get a quotient N and a remainder (which we ignore).
    var n = Math.floor((h - m + l + 90) / 25);

    // Divide H – M + L + N + 19 by 32 to get a quotient (which we ignore) and a remainder P.Easter Sunday is the Pth day of the Nth month (N can be either 3 for March or 4 for April). The year’s dominical letter can be found by dividing 2E + 2J – K by 7 and taking the remainder (a remainder of 0 is equivalent to the letter A, 1 is equivalent to B, and so on).
    var p = (h - m + l + n + 19) % 32;

    // get day and month
    var day = p + 1;// p is easter (VELIKA NOČ)
    var month = n - 1;// cos of javascript getMonth() 

    // return month and day
    return {
        month: month,
        day: day,
        name: "VELIKONOČNI PONEDELJEK"
    };
};

module.exports = isSloveniaHoliday;