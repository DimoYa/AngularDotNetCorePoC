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
  ],
  entryComponents: [AddSkillComponent, EditSkillLevelComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
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
    ]),
    BrowserAnimationsModule,
    MatExpansionModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
