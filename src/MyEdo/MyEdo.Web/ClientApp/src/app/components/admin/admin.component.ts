import { Component, OnInit } from "@angular/core";
import { PageEvent } from "@angular/material/paginator";
import UserModel from "../../core/models/user-model";
import { AdminService } from "../../core/services/admin.service";

@Component({
  selector: "app-admin",
  templateUrl: "./admin.component.html",
  styleUrls: ["./admin.component.css"],
})
export class AdminComponent implements OnInit {
  public users: UserModel[];
  public pageSlice: UserModel[];

  constructor(private adminService: AdminService) {}

  ngOnInit() {
    this.adminService.getAllUsers().subscribe((data) => {
      this.users = data
      this.pageSlice = this.users.slice(0, 5);
    });
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

  public OnPageChange(event: PageEvent) {
    const startIndex = event.pageIndex * event.pageSize;
    let endIndex = startIndex + event.pageSize;
    if (endIndex > this.users.length) {
      endIndex = this.users.length;
    }
    this.pageSlice = this.users.slice(startIndex, endIndex);
  }
}
