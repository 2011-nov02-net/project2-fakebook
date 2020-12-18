import { User } from './user';
import { Comment } from './comment';

export interface Post {
  id: number;
  content: string;
  user: User;
  pictureUrl: string | undefined;
  createdAt: Date;
  likedByUserIds: number[];
  commentIds: number[];
  comments: Comment[];
}
