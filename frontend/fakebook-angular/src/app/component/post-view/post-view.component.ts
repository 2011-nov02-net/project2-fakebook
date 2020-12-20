import { Component, OnInit, Input } from '@angular/core';
import { Post } from '../../model/post';
import { Comment } from 'src/app/model/comment';

@Component({
  selector: 'app-post-view',
  templateUrl: './post-view.component.html',
  styleUrls: ['./post-view.component.css']
})
export class PostViewComponent implements OnInit {
  @Input() post : Post | null = null;
  constructor() { }

  ngOnInit(): void {
  }

}
