import { JobTitle } from "src/app/common/enums/job-title.enum";
import { BaseModel } from "../generic/base.model";

export interface UserModel extends BaseModel {
    email: string;
    picture?: string;
    jobTitleId: JobTitle;
    department: string;
}