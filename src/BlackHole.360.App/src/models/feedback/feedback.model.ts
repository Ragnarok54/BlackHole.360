import { BaseModel } from "../generic/base.model";

export interface FeedbackModel {
    id?: string;
    content: string;
    isAnonymous: boolean;
    toUserId?: string;
    toUser?: string;
    createdOn?: Date;
}