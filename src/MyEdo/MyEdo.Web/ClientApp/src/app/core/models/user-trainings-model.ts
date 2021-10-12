export interface UserTrainingModel {
    userId: string;
    userName: string;
    trainings: TrainingModel[]
}

interface TrainingModel {
    trainingId: string;
    trainingName: string;
    status: number;
    dueDate: Date
}