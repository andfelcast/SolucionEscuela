import { Subject } from "./subject";
export interface Teacher{
    id:number;
    name:string;
    active:string;
    subjects:Subject[];
}