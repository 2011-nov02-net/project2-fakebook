import { User } from "./user";

export interface Comment {
  id: number;
  userId: number;
  content: string;
  postId: number;
  parentCommentId: number | undefined;
  createdAt: Date | undefined;
  childCommentIds: number[];
  user: User | undefined;
}
