import { UserModel } from "./user.model";

export class MessageModel {
    message: string;
    userId: number;
    user: UserModel;
    roomId: number;
}