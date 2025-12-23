import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { Dashboard } from './components/dashboard/dashboard';
import { StudentInfo } from './components/dashboard/student-info/student-info';
import { SubjectInfo } from './components/dashboard/subject-info/subject-info';
import { AddSubjects } from './components/dashboard/add-subjects/add-subjects';

export const routes: Routes = [
    { path: "", component:Login},
    { path:"register",component:Register},
    { path:"home",component:Dashboard,
        children: [
            {path: 'student', component: StudentInfo},
            {path: 'subjects', component: SubjectInfo},
            {path: 'addSubjects', component: AddSubjects},
        ],
    }
];
