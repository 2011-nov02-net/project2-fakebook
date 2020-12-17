export interface Post {
  id: number;
  content: string;
  userId: number;
  pictureUrl: string | undefined;
  createdAd: Date;
  likedByUserIds: number[];
  commentIds: number[];
}
