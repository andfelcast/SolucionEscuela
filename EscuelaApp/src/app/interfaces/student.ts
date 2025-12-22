import { Subject } from "./subject";

export interface StudentRegister{
    firstName:string;
    lastName:string;
    birthDate:Date,
    city:string;
    email:string;
    address:string;
    phone:string;
    password:string;
}

export interface Student extends StudentRegister{
    id:number;
    userName:string;
    credits:string;
    active:boolean;
    creationDate:Date;
    Subjects:Subject[];

}

