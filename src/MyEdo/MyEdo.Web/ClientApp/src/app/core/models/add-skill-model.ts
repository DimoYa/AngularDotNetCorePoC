export interface AddSkillModel {
    skillId: string;
    skillName: string;
    skillLevel: SkillLevel
}

export enum SkillLevel {
    Beginning = 1,
    Medium = 2,
    UpperMedium = 3,
    Advanced = 4,
}