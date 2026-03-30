export interface Task {
  id: number;
  title: string;
  description: string | null;
  isCompleted: boolean;
  createdAt: string;
}

export interface TaskCreate {
  title: string;
  description: string | null;
  isCompleted: boolean;
}

export interface TaskUpdate {
  title: string;
  description: string | null;
  isCompleted: boolean;
}
