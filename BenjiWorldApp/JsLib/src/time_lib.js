import moment from 'moment';
import countdown from 'countdown';

export function getCurrentTime() {
    return moment().format('MMMM Do YYYY, h:mm a');
}

//export function getRelativeTime(date) {
//    moment.updateLocale('en', {
//        relativeTime: (number, withoutSuffix, key, isFuture) => {
//            return "testing " + number + " | " + withoutSuffix + " | "+ key + " | " + isFuture;
//        }
//    });
//    console.log("getRelativeTime 1 called with date: ", date, moment(date).fromNow("s"));
//    return moment(date).fromNow("s");
//}

export function getRelativeTime(date) {
    console.log("getRelativeTime 1 called with date: ", date, countdown.SECONDS);
    return countdown(new Date(date), null, ~(countdown.HOURS | countdown.MINUTES |countdown.SECONDS | countdown.MILLISECONDS)).toString();
}