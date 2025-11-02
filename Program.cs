//*****************************************************************************
//** 47. Permutations II                                            leetcode **
//*****************************************************************************
//** Some numbers twin, yet twist anew,                                      **
//** Each mirrored path still finds its view.                                **
//** Through swaps and checks, the copies fade,                              **
//** Till every fate unique is made.                                         **
//*****************************************************************************

/**
 * Return an array of arrays of size *returnSize.
 * The sizes of the arrays are returned as *returnColumnSizes array.
 * Note: Both returned array and *columnSizes array must be malloced, assume caller calls free().
 */
int cmp(const void* a, const void* b)
{
    return (*(int*)a - *(int*)b);
}

void swap(int* a, int* b)
{
    int tmp = *a;
    *a = *b;
    *b = tmp;
}

void backtrack(int* nums, int numsSize, int** result, int* returnSize, int** returnColumnSizes, int start)
{
    if (start == numsSize)
    {
        int* permutation = (int*)malloc(numsSize * sizeof(int));
        for (int i = 0; i < numsSize; i++)
            permutation[i] = nums[i];

        result[*returnSize] = permutation;
        (*returnColumnSizes)[*returnSize] = numsSize;
        (*returnSize)++;
        return;
    }

    int used[21] = {0};  // Values range from -10 to 10, offset by +10 for indexing
    for (int i = start; i < numsSize; i++)
    {
        int idx = nums[i] + 10;
        if (used[idx])
            continue;
        used[idx] = 1;

        swap(&nums[start], &nums[i]);
        backtrack(nums, numsSize, result, returnSize, returnColumnSizes, start + 1);
        swap(&nums[start], &nums[i]);
    }
}

int** permuteUnique(int* nums, int numsSize, int* returnSize, int** returnColumnSizes)
{
    qsort(nums, numsSize, sizeof(int), cmp);  // Sort to group duplicates, helps pruning

    // Upper bound on permutations = n! (for n ≤ 8 → 40320 max)
    int maxPerms = 40320;

    int** retval = (int**)malloc(maxPerms * sizeof(int*));
    *returnColumnSizes = (int*)malloc(maxPerms * sizeof(int));
    *returnSize = 0;

    backtrack(nums, numsSize, retval, returnSize, returnColumnSizes, 0);

    return retval;
}