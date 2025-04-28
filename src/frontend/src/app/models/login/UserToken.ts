export class UserToken {
    token: string;
    expiration: Date;

    constructor() {
        this.token = "";
        this.expiration = new Date();
    }
}