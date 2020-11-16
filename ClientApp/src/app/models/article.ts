export class Article {
    id: number
    title: string
    description: string
    content: string
    image: string
    createdAt: string
    viewsCount: number
    likesCount: number
    commentsCount: number
    likeIsPutted: boolean
    categoryIds: number[]
}