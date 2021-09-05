import { Component, OnInit } from "@angular/core";
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

  public onLockChange(userId: string, isLocked: boolean): void {
    isLocked
      ? this.adminService.unLockUser(userId).subscribe()
      : this.adminService.lockUser(userId).subscribe();

    this.users
      .filter((u) => u.id == userId)
      .map((u) => (u.isLocked = !isLocked));
  }

  public onRoleChange(userId: string, roleName: string, shouldAddRole: boolean): void {

    shouldAddRole
      ? this.adminService.addRoleToUser(userId, roleName).subscribe()
      : this.adminService.removeRoleFromUser(userId, roleName).subscribe();

    this.users
      .filter((r) => r.id == userId)
      .map((s) => s.roles)[0]
      .push({
        id: "123",
        name: roleName
      });
  }

  public isResource(userId: string): boolean {
    return this.users
      .filter((r) => r.id == userId)
      .map((s) => s.roles)[0]
      .some((r) => r.name == "Resource");
  }

  public isAdmin(userId: string): boolean {
    return this.users
      .filter((r) => r.id == userId)
      .map((s) => s.roles)[0]
      .some((r) => r.name == "Administrator");
  }
}
