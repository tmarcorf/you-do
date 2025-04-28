export class ToDo {
    id: string;
    title: string;
    details: string;
    createdAt: Date;
    updatedAt: Date;
    completed: boolean;
  
    constructor(
      id: string,
      title: string,
      details: string,
      createdAt: Date,
      updatedAt: Date,
      completed: boolean
    ) {
      this.id = id;
      this.title = title;
      this.details = details;
      this.createdAt = createdAt;
      this.updatedAt = updatedAt;
      this.completed = completed;
    }
  }
  