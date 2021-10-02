export interface TrainingModel {
  id: string;
  name: string;
  type: TrainingType;
  status: TrainingStatus;
  dueDate: Date;
}

enum TrainingType {
  Optional = 1,
  Mandatory = 2,
}

enum TrainingStatus {
  Active = 1,
  Inactive = 2,
}
