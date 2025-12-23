import { Subject } from "./subject";

export interface StudentRegister{
    firstName:string;
    lastName:string;
    documentNumber:string,
    city:string;
    email:string;
    address:string;
    phone:string;
    password:string;
}

export interface Student extends StudentRegister{
    id:number;
    userName:string;
    credits:number;
    active:boolean;
    creationDate:Date;
    subjects:Subject[];

}

