export class InputOrderFormDto {
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