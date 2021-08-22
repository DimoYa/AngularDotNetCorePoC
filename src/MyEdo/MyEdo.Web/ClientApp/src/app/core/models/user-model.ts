import RoleModel from "./role-model";

interface UserModel {
    id: string;
    userName: string,
    fullName: string,
    isLocked: boolean,
    roles: RoleModel[]
}
export default UserModel;