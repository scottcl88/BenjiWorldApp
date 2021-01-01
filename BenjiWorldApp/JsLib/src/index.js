import { getCurrentTime, getRelativeTime } from './time_lib';

export function GetCurrentTime() {
    return getCurrentTime();
}

export function GetRelativeTime(date) {
    console.log("GetRelativeTime called with date: ", date);
    return getRelativeTime(date);
}