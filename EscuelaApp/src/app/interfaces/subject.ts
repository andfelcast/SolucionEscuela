import { Teacher } from "./teacher";

export interface Subject{
    id:number;
    name:string;
    description:string;
    credits:number;
    teacherId:number;
    teacher:Teacher;

}