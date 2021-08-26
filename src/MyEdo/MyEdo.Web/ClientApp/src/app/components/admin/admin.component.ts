import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import UserModel from "../../core/models/user-model";
import { AdminService } from "../../core/services/admin.service";

@Component({
  selector: "app-admin",
  templateUrl: "./admin.component.html",
  styleUrls: ["./admin.component.css"],
})
export class AdminComponent implements OnInit {
  users: UserModel[];

  constructor(private adminService: AdminService) {}

  ngOnInit() {
    this.adminService.getAllUsers().subscribe((data) => (this.users = data));
  }

  public async onSaveChanged(userId: string, lockUser: boolean): void {
    lockUser
      ? this.adminService.unLockUser(userId).subscribe()
      : this.adminService.lockUser(userId).subscribe();
    this.users
      .filter((u) => u.id == userId)
      .map((p) => (p.isLocked = !lockUser));
  }
}
