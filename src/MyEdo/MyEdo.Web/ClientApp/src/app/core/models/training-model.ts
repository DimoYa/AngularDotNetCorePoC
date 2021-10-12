export interface TrainingModel {
  id: string;
  name: string;
  type: TrainingType;
  status: TrainingStatus;
  dueDate: Date;
}

export enum TrainingType {
  Optional = 1,
  Mandatory = 2,
}

export enum TrainingStatus {
  Active = 1,
  Inactive = 2,
}

