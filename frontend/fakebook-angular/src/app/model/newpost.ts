import { User } from './user';

export class newPost {
    constructor(
    public content: string,
    public userId: number | undefined,
    public pictureUrl: string | undefined
    )
    {}
}
