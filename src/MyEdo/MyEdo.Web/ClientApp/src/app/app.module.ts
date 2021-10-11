import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./components/shared/nav-menu/nav-menu.component";
import { HomeComponent } from "./components/shared/home/home.component";
import { ApiAuthorizationModule } from "../api-authorization/api-authorization.module";
import { AuthorizeGuard } from "../api-authorization/authorize.guard";
import { AuthorizeInterceptor } from "..//api-authorization/authorize.interceptor";
import { FooterComponent } from "./components/shared/footer/footer.component";
import { AdminComponent } from "./components/admin/admin.component";
import { AllSkillsComponent } from "./components/skill/all-skills/all-skills.component";
import { SkillComponent } from "./components/skill/skill/skill.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatExpansionModule } from "@angular/material/expansion";
import {
  ConfirmBoxConfigModule,
  DialogConfigModule,
  NgxAwesomePopupModule,
} from "@costlydeveloper/ngx-awesome-popup";
import { AddSkillComponent } from "./components/skill/add-skill/add-skill.component";
import { MySkillsComponent } from "./components/skill/my-skills/my-skills.component";
import { EditSkillLevelComponent } from "./components/skill/edit-skill-level/edit-skill-level.component";
import { CreateCategoryComponent } from "./components/skill/create-category/create-category.component";
import { EditCategoryComponent } from "./components/skill/edit-category/edit-category.component";
import { EditSkillComponent } from "./components/skill/edit-skill/edit-skill.component";
import { CreateSkillComponent } from "./components/skill/create-skill/create-skill.component";
import { AllTrainingnsComponent } from "./components/training/all-trainingns/all-trainingns.component";
import { CreateTrainingComponent } from "./components/training/create-training/create-training.component";
import { MatInputModule } from "@angular/material/input";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatNativeDateModule } from "@angular/material/core";
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material';
import { MomentDateModule, MomentDateAdapter } from '@angular/material-moment-adapter';
import { MY_FORMATS } from "./core/common/date-picker-format";
import { EditTrainingComponent } from './components/training/edit-training/edit-training.component';
import { MyTrainingsComponent } from './components/training/my-trainings/my-trainings.component';
import { AssignTrainingComponent } from './components/training/assign-training/assign-training.component';
import { UserTrainingsComponent } from './components/training/user-trainings/user-trainings.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FooterComponent,
    AdminComponent,
    AllSkillsComponent,
    SkillComponent,
    AddSkillComponent,
    MySkillsComponent,
    EditSkillLevelComponent,
    CreateCategoryComponent,
    EditCategoryComponent,
    EditSkillComponent,
    CreateSkillComponent,
    AllTrainingnsComponent,
    CreateTrainingComponent,
    EditTrainingComponent,
    MyTrainingsComponent,
    AssignTrainingComponent,
    UserTrainingsComponent,
  ],
  entryComponents: [AddSkillComponent, EditSkillLevelComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    MatInputModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MomentDateModule,
    FormsModule,
    NgxAwesomePopupModule,
    DialogConfigModule,
    ApiAuthorizationModule,
    ReactiveFormsModule,
    NgxAwesomePopupModule.forRoot(),
    ConfirmBoxConfigModule.forRoot(),
    NgxAwesomePopupModule.forRoot(),
    DialogConfigModule.forRoot(),
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
      {
        path: "admin",
        component: AdminComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "all-skills",
        component: AllSkillsComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "my-skills",
        component: MySkillsComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "create-category",
        component: CreateCategoryComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "categories/edit/:id",
        component: EditCategoryComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "create-skill",
        component: CreateSkillComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "all-skills/edit/:id",
        component: EditSkillComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "all-trainings",
        component: AllTrainingnsComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "create-training",
        component: CreateTrainingComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "all-trainings/edit/:id",
        component: EditTrainingComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "my-trainings",
        component: MyTrainingsComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "all-trainings/assign/:id",
        component: AssignTrainingComponent,
        canActivate: [AuthorizeGuard],
      },
      {
        path: "user-trainings",
        component: UserTrainingsComponent,
        canActivate: [AuthorizeGuard],
      },
    ]),
    BrowserAnimationsModule,
    MatExpansionModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
