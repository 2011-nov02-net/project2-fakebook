export interface Post {
  id: number;
  content: string;
  userId: number;
  firstName: string;
  lastLast: string;
  pictureUrl: string | undefined;
  createdAd: Date;
  likedByUserIds: number[];
  commentIds: number[];
}
