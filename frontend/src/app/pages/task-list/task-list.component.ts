import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Task } from '../../models/task';
import { TaskService } from '../../services/task.service';

@Component({
  selector: 'app-task-list',
  imports: [CommonModule, RouterLink],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.css'
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];
  isLoading = false;
  errorMessage: string | null = null;

  constructor(
    private readonly taskService: TaskService,
    private readonly cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.isLoading = true;
    this.errorMessage = null;

    this.taskService.getTasks().subscribe({
      next: (data) => {
        this.tasks = Array.isArray(data) ? data : [];
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.tasks = [];
        this.isLoading = false;
        this.errorMessage = 'Unable to load tasks right now. Please refresh and try again.';
        this.cdr.detectChanges();
      }
    });
  }

  deleteTask(id: number): void {
    this.taskService.deleteTask(id).subscribe({
      next: () => this.loadTasks(),
      error: () => this.loadTasks()
    });
  }

  toggleStatus(task: Task): void {
    this.taskService.updateTask(task.id, {
      title: task.title,
      description: task.description,
      isCompleted: !task.isCompleted
    }).subscribe({
      next: () => this.loadTasks(),
      error: () => this.loadTasks()
    });
  }
}
