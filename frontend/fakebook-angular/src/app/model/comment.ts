export interface Comment {
  id: number;
  userId: number;
  content: string;
  postId: number;
  parentCommentId: number | undefined;
  createdAt: Date;
  childCommentIds: number[];
}
