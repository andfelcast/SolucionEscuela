export interface GeneralRequest{
    id:string;
    type:string;
    body:any;
}

export interface GeneralResponse{
    isValid:boolean;
    message:string;
    resultData:any;
}