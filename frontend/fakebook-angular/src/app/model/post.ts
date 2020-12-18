import { User } from './user';

export interface Post {
  id: number;
  content: string;
  user: User;
  pictureUrl: string | undefined;
  createdAt: Date;
  likedByUserIds: number[];
  commentIds: number[];
}
