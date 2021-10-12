export interface AllUsersTrainingsModel {
  trainingId: string;
  trainingName: string;
  userId: string;
  userName: string;
  status: UserTrainingStatus;
  dueDate: Date
}

export enum UserTrainingStatus {
  Requested = 1,
  Assigned = 2,
  Rejected = 3,
  Passed = 4,
  Failed = 5,
}
