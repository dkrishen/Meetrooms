export class InputBookingFormDto {
    username?: string;
    startTime?: string;
    endTime?: string;
    date?: string;

    constructor(){
        this.username = "";
        this.startTime = "";
        this.endTime = "";
        this.date = "";
    }
}
