import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
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
  ],
  entryComponents: 
  [
    AddSkillComponent,
    EditSkillLevelComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    NgxAwesomePopupModule,
    DialogConfigModule,
    ApiAuthorizationModule,
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
