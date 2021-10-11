export interface UserTrainingModel {
    userId: string;
    userName: string;
    trainings: SkillModel[]
}

interface SkillModel {
    trainingId: string;
    trainingName: string;
    status: number;
}