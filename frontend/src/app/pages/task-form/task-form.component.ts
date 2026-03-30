import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { TaskService } from '../../services/task.service';

@Component({
  selector: 'app-task-form',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './task-form.component.html',
  styleUrl: './task-form.component.css'
})
export class TaskFormComponent implements OnInit {
  taskId: number | null = null;
  title = 'Add Task';
  taskForm;

  constructor(
    private readonly fb: FormBuilder,
    private readonly taskService: TaskService,
    private readonly route: ActivatedRoute,
    private readonly router: Router
  ) {
    this.taskForm = this.fb.nonNullable.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      description: ['', [Validators.maxLength(1000)]],
      isCompleted: false
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.taskId = Number(idParam);
      this.title = 'Edit Task';
      this.taskService.getTaskById(this.taskId).subscribe((task) => {
        this.taskForm.patchValue({
          title: task.title,
          description: task.description ?? '',
          isCompleted: task.isCompleted
        });
      });
    }
  }

  saveTask(): void {
    if (this.taskForm.invalid) {
      this.taskForm.markAllAsTouched();
      return;
    }

    const formValue = this.taskForm.getRawValue();
    const payload = {
      title: formValue.title,
      description: formValue.description === '' ? null : formValue.description,
      isCompleted: formValue.isCompleted
    };

    if (this.taskId === null) {
      this.taskService.createTask(payload).subscribe(() => {
        this.router.navigate(['/tasks']);
      });
      return;
    }

    this.taskService.updateTask(this.taskId, payload).subscribe(() => {
      this.router.navigate(['/tasks']);
    });
  }
}
