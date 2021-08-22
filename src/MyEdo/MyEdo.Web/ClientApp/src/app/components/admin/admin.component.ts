import { Component, OnInit } from "@angular/core";
import UserModel from "src/app/core/models/user-model";
import { AdminService } from "src/app/core/services/admin.service";

@Component({
  selector: "app-admin",
  templateUrl: "./admin.component.html",
  styleUrls: ["./admin.component.css"],
})
export class AdminComponent implements OnInit {
  public users: UserModel[];

  constructor(private adminService: AdminService) {}

  ngOnInit() {
    this.adminService.getAllUsers().subscribe((data) => {
      this.users = data;
      console.log(this.users);
    });
  }

  public onSaveChanged(userId: string, lockUser: boolean): void {
    lockUser
      ? this.adminService.lockUser(userId).subscribe((response) => {})
      : this.adminService.unLockUser(userId).subscribe((response) => {});
  }
}
